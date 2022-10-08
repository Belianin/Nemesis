import React, { CSSProperties } from 'react';
import { useNavigate } from 'react-router';
import { useUser } from '../UserContextProvider';

const style: CSSProperties = {
    boxShadow: "0 2px 2px -2px gray",
    width: "100vw",
    display: "flex",
    justifyContent: "space-between",
}

export const Header: React.FC = () => {

    const {user} = useUser();
    
    const navigate = useNavigate();

    const userContent = user !== undefined
        ? <p>Hi, {user.login}!</p>
        : <button onClick={() => navigate("/login")}>LogIn or Register</button>

    return <div style={style}>
        <h4 onClick={() => navigate("/")} style={{cursor: "pointer"}}>Nemesis Online</h4>
        {userContent}
    </div>
}