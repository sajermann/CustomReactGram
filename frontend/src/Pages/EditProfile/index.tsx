/* eslint-disable jsx-a11y/label-has-associated-control */
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { Message } from '../../Components/Message';
import { login, reset } from '../../Slices/authSlice';
import {
	profile,
	resetMessage,
	updateProfile,
	updateProfileImage,
} from '../../Slices/userSlice';
import { uploads } from '../../Utils/config';
import './index.css';

export default function EditProfile() {
	const [name, setName] = useState('');
	const [email, setEmail] = useState('');
	const [bio, setBio] = useState('');
	const [password, setPassword] = useState('');
	const [profileImage, setProfileImage] = useState('');
	const [previewImage, setPreviewImage] = useState<Blob | MediaSource | null>(
		null
	);
	const dispatch = useDispatch();
	const { user, message, error, loading } = useSelector(
		(state: any) => state.user
	);
	console.log({ user });

	useEffect(() => {
		// @ts-expect-error Esperado
		dispatch(profile());
	}, [dispatch]);

	useEffect(() => {
		if (user) {
			setName(user.name);
			setEmail(user.email);
			setBio(user.bio);
			setProfileImage(user.profileImage);
		}
	}, [user]);

	async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		const userData: any = {
			name,
			bio,
			profileImage,
		};

		if (profileImage) {
			userData.profileImage = profileImage;
		}

		if (bio !== '') {
			userData.bio = bio;
		}

		// const formData = new FormData();

		// const userFormData = Object.keys(userData).forEach(key =>
		// 	formData.append(key, userData[key])
		// );

		// formData.append('user', userFormData);

		// @ts-expect-error Esperado
		await dispatch(updateProfile(userData));

		setTimeout(() => {
			dispatch(resetMessage());
		}, 2000);
	}

	async function handleFile(e: any) {
		const image = e.target.files[0];
		setPreviewImage(image);
		setProfileImage(image);

		const formData = new FormData();
		formData.append('file', e.target.files[0]);
		console.log({ formData });
		// @ts-expect-error Esperado
		await dispatch(updateProfileImage(formData));
	}

	return (
		<div id="edit-profile">
			<h2>Edite seus dados</h2>
			<p className="subtitle">
				Adicione uma image de perfil e conta mais sobre você...
			</p>
			{(user.profileImage || previewImage) && (
				<img
					className="profile-image"
					src={
						previewImage
							? URL.createObjectURL(previewImage)
							: `${uploads}/users/${user.profileImage}`
					}
					alt={user.name}
				/>
			)}
			<form onSubmit={handleSubmit}>
				<input
					onChange={e => setName(e.target.value)}
					type="text"
					placeholder="Nome"
					value={name}
				/>
				<input
					onChange={e => setEmail(e.target.value)}
					type="text"
					placeholder="Email"
					value={email}
					disabled
				/>
				<label>
					<span>Imagem do Perfil</span>
					<input type="file" onChange={handleFile} />
				</label>
				<label>
					<span>Bio</span>
					<input
						onChange={e => setBio(e.target.value)}
						type="text"
						placeholder="Descrição do Perfil"
						value={bio}
					/>
				</label>
				<label>
					<span>Quer alterar sua senha?</span>
					<input
						onChange={e => setPassword(e.target.value)}
						type="password"
						placeholder="Digite sua nova senha"
						disabled
						value={password}
					/>
				</label>

				<input
					disabled={loading}
					type="submit"
					value={loading ? 'Aguarde...' : 'Atualizar'}
				/>
				{message && <Message type="success" msg={message} />}
				{error && <Message type="error" msg={error} />}
			</form>
		</div>
	);
}
