/* eslint-disable jsx-a11y/label-has-associated-control */
import React, { useEffect, useRef, useState } from 'react';
import { BsFillEyeFill, BsPencilFill, BsXLg } from 'react-icons/bs';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useParams } from 'react-router-dom';
import { Message } from '../../Components/Message';
import {
	deletePhoto,
	getUserPhotos,
	publishPhoto,
	resetMessage,
	updatePhoto,
} from '../../Slices/photoSlice';
import { getUserDetails } from '../../Slices/userSlice';
import { uploads } from '../../Utils/config';

import './index.css';

export default function Profile() {
	const { id } = useParams<{ id: string }>();
	const dispatch = useDispatch();
	const { user, loading } = useSelector((state: any) => state.user);
	const { user: userAuth } = useSelector((state: any) => state.auth);
	const {
		photos,
		photo,
		loading: loadingPhoto,
		message: messagePhoto,
		error: errorPhoto,
	} = useSelector((state: any) => state.photo);
	const [title, setTitle] = useState('');
	const [editId, setEditId] = useState('');
	const [editTitle, setEditTitle] = useState('');
	const [editImage, setEditImage] = useState('');
	const [image, setImage] = useState<File | null>(null);
	const newPhotoForm = useRef<any>(null);
	const editPhotoForm = useRef<any>(null);

	useEffect(() => {
		// @ts-expect-error Esperado
		dispatch(getUserDetails(id));
		// @ts-expect-error Esperado
		dispatch(getUserPhotos(id));
	}, [dispatch, id]);

	function resetComponentMessage() {
		setTimeout(() => {
			dispatch(resetMessage());
		}, 2000);
	}

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		const formData = new FormData();
		formData.append('Title', title);
		formData.append('file', image as Blob);

		// @ts-expect-error Esperado
		await dispatch(publishPhoto(formData));
		resetComponentMessage();
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

	async function handleDelete(photoId: string) {
		// @ts-expect-error Esperado
		await dispatch(deletePhoto(photoId));
		resetComponentMessage();
	}

	function showOrHideForms() {
		newPhotoForm.current.classList.toggle('hide');
		editPhotoForm.current.classList.toggle('hide');
	}

	function handleCancelEdit() {
		showOrHideForms();
	}

	function handleUpdate(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		const photoTemp = {
			title: editTitle,
			id: editId,
		};
		// @ts-expect-error esperado
		dispatch(updatePhoto(photoTemp));

		resetComponentMessage();
	}

	function handleEdit(item: any) {
		if (editPhotoForm.current.classList.contains('hide')) {
			showOrHideForms();
		}

		setEditId(item.id);
		setEditTitle(item.title);
		setEditImage(item.image);
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
				<>
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
					<div className="edit-photo hide" ref={editPhotoForm}>
						<p>Editando</p>
						{editImage && (
							<img src={`${uploads}/photos/${editImage}`} alt={editTitle} />
						)}
						<form onSubmit={handleUpdate}>
							<input
								type="text"
								onChange={e => setEditTitle(e.target.value)}
								value={editTitle}
							/>

							<input
								disabled={loading}
								type="submit"
								value={loading ? 'Aguarde...' : 'Atualizar'}
							/>
							<button
								type="button"
								className="cancel-btn"
								onClick={handleCancelEdit}
							>
								Cancelar edição
							</button>
							{messagePhoto && <Message type="success" msg={messagePhoto} />}
							{errorPhoto && <Message type="error" msg={errorPhoto} />}
						</form>
					</div>
				</>
			)}
			<div className="user-photos">
				<h2>Fotos publicadas</h2>
				<div className="photos-container">
					{photos &&
						photos.map((item: any) => (
							<div className="photo" key={item.id}>
								{item.image && (
									<img
										src={`${uploads}/photos/${item.image}`}
										alt={item.title}
									/>
								)}
								{id === user.id ? (
									<div className="actions">
										<Link to={`/photos/${item.id}`}>
											<BsFillEyeFill />
										</Link>
										<BsPencilFill onClick={() => handleEdit(item)} />
										<BsXLg onClick={() => handleDelete(item.id)} />
									</div>
								) : (
									<Link className="btn" to={`/photos/${item.id}`}>
										Ver
									</Link>
								)}
							</div>
						))}
				</div>
				{photos.length === 0 && <p>Ainda não há photos publicadas</p>}
			</div>
		</div>
	);
}
