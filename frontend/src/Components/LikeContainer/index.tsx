import { BsHeart, BsHeartFill } from 'react-icons/bs';
import './index.css';

type Props = {
	photo: any;
	user: any;
	handleLike: (data: any) => void;
};

export function LikeContainer({ photo, user, handleLike }: Props) {
	console.log({ photo });
	return (
		<div className="like">
			{photo && user && (
				<>
					{photo.likes && photo.likes.includes(user.id) ? (
						<BsHeartFill onClick={() => handleLike(photo)} />
					) : (
						<BsHeart onClick={() => handleLike(photo)} />
					)}
					<p>{photo.likes && photo.likes.length} like(s)</p>
				</>
			)}
		</div>
	);
}
