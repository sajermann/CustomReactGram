import { BrowserRouter } from 'react-router-dom';

import './App.css';
import { Footer } from './Components/Footer';
import { Navbar } from './Components/Navbar';
import Routes from './Pages/Routes';

function App() {
	return (
		<BrowserRouter>
			<Navbar />
			<div className="container">
				<Routes />
			</div>
			<Footer />
		</BrowserRouter>
	);
}

export default App;
