/* eslint-disable jsx-a11y/label-has-associated-control */
import React, { useEffect, useRef, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { Message } from '../../Components/Message';
import { publishPhoto, resetMessage } from '../../Slices/photoSlice';
import { getUserDetails } from '../../Slices/userSlice';
import { uploads } from '../../Utils/config';

import './index.css';

export default function Profile() {
	const { id } = useParams<{ id: string }>();
	const dispatch = useDispatch();
	const { user, loading } = useSelector((state: any) => state.user);
	const { user: userAuth } = useSelector((state: any) => state.auth);
	const {
		photo,
		loading: loadingPhoto,
		message: messagePhoto,
		error: errorPhoto,
	} = useSelector((state: any) => state.photo);
	const [title, setTitle] = useState('');
	const [image, setImage] = useState<File | null>(null);
	const newPhotoForm = useRef<any>(null);
	const editPhotoForm = useRef<any>(null);

	useEffect(() => {
		// @ts-expect-error Esperado
		dispatch(getUserDetails(id));
	}, [dispatch, id]);

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		const formData = new FormData();
		formData.append('Title', title);
		formData.append('file', image as Blob);

		// @ts-expect-error Esperado
		await dispatch(publishPhoto(formData));

		setTimeout(() => {
			dispatch(resetMessage());
		}, 2000);
	}

	async function handleFile(e: React.ChangeEvent<HTMLInputElement>) {
		if (!e || !e.target || !e.target.files) {
			return;
		}

		setImage(e.target.files[0]);
	}

	if (loading) {
		return <p>Carregando...</p>;
	}

	return (
		<div id="profile">
			<div className="profile-header">
				{user.profileImage && (
					<img src={`${uploads}/users/${user.profileImage}`} alt={user.name} />
				)}
				<div className="profile-description">
					<h2>{user.name}</h2>
					<p>{user.bio}</p>
				</div>
			</div>

			{id === userAuth.id && (
				<div className="new-photo" ref={newPhotoForm}>
					<h3>Compartilhe algum momento seu:</h3>
					<form onSubmit={handleSubmit}>
						<label>
							<span>Título para foto</span>
							<input
								type="text"
								placeholder="Insira um título"
								onChange={e => setTitle(e.target.value)}
								value={title}
							/>
						</label>
						<label>
							<span>Imagem</span>
							<input type="file" onChange={handleFile} />
						</label>

						<input
							disabled={loading}
							type="submit"
							value={loading ? 'Aguarde...' : 'Postar'}
						/>
						{messagePhoto && <Message type="success" msg={messagePhoto} />}
						{errorPhoto && <Message type="error" msg={errorPhoto} />}
					</form>
				</div>
			)}
		</div>
	);
}
