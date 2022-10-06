import { Link } from 'react-router-dom';
import { uploads } from '../../Utils/config';
import './index.css';

type Props = {
	photo: any;
};

export function PhotoItem({ photo }: Props) {
	return (
		<div className="photo-item">
			{photo.image && (
				<img src={`${uploads}/photos/${photo.image}`} alt={photo.title} />
			)}
			<h2>{photo.title}</h2>
			<p className="photo-author">
				Publicada por:{' '}
				<Link to={`/users/${photo.userId}`}>{photo.userName}</Link>
			</p>
		</div>
	);
}
