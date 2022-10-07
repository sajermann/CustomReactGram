import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useParams } from 'react-router-dom';
import { LikeContainer } from '../../Components/LikeContainer';
import { Message } from '../../Components/Message';
import { PhotoItem } from '../../Components/PhotoItem';
import { useResetComponentMessage } from '../../Hooks/UseResetComponentMessage';
import { comment, getPhotoById, like } from '../../Slices/photoSlice';
import { uploads } from '../../Utils/config';
import './index.css';

export default function Photo() {
	const { id } = useParams<{ id: string }>();
	const dispatch = useDispatch();
	const resetMessage = useResetComponentMessage(dispatch);
	const { user } = useSelector((state: any) => state.auth);
	const { photo, loading, error, message } = useSelector(
		(state: any) => state.photo
	);
	const [commentText, setCommentText] = useState('');

	useEffect(() => {
		// @ts-expect-error Esperado
		dispatch(getPhotoById(id));
	}, [dispatch, id]);

	function handleLike() {
		// @ts-expect-error Esperado
		dispatch(like(photo.id));
		resetMessage();
	}

	function handleComment(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		const commentData = {
			comment: commentText,
			id: photo.id,
		};

		// @ts-expect-error esperado
		dispatch(comment(commentData));

		setCommentText('');

		resetMessage();
	}

	if (loading) {
		return <p>Carregando...</p>;
	}

	return (
		<div id="photo">
			<PhotoItem photo={photo} />
			<LikeContainer photo={photo} user={user} handleLike={handleLike} />
			<div className="message-container">
				{error && <Message msg={message} type="error" />}
				{message && <Message msg={message} type="success" />}
			</div>

			<div className="comments">
				<h3>Comentários: ({photo.comments ? photo.comments.length : 0})</h3>
				<form onSubmit={handleComment}>
					<input
						type="text"
						placeholder="Insira o seu comentário..."
						onChange={e => setCommentText(e.target.value)}
					/>
					<input type="submit" value="Adicionar" />
				</form>
				{!photo.comments && <p>Não há comentários</p>}
				{photo.comments &&
					photo.comments.map((commentTemp: any) => (
						<div className="comment" key={commentTemp.comment}>
							<div className="author">
								{commentTemp.userImage && (
									<img
										src={`${uploads}/users/${commentTemp.userImage}`}
										alt={commentTemp.userName}
									/>
								)}
								<Link to={`/users/${commentTemp.userId}`}>
									<p>{commentTemp.userName}</p>
								</Link>
							</div>
							<p>{commentTemp.comment}</p>
						</div>
					))}
			</div>
		</div>
	);
}
