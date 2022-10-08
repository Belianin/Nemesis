import React, { CSSProperties } from 'react';
import {BrowserRouter} from "react-router-dom";
import {Routes} from "./Routes";
import { Header } from './user/Header';
import { UserContextProvider } from './UserContextProvider';

const containerStyle: CSSProperties = {
    display: 'flex',
    flexDirection: "column",
    justifyContent: "flex-start",
    alignItems: "center"
}

function App() {

    return (
        <UserContextProvider>
            <BrowserRouter>
                <div style={containerStyle}>
                    <Header />
                    <Routes />
                </div>
            </BrowserRouter>
        </UserContextProvider>

    );
}

export default App;
