import React, { useState } from 'react';
import { useNavigate } from 'react-router';
import { logIn, register } from '../api/usersApi';
import { Input } from '../common/Input';
import { useUser } from '../UserContextProvider';

export const RegisterForm: React.FC = () => {

    const {handleSession} = useUser();

    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    function handleRegister() {
        register({
            login,
            password
        }).then(() => {
            logIn({
                login,
                password
            }).then(response => handleSession(response.sessionId)
            .then(() => navigate('/')))
        })
    }

    return <div>    
        <h1>Register</h1>
        <p>Login</p>
        <Input value={login} onChange={setLogin}/>
        <p>Password</p>
        <input type="password" value={password} onChange={x => setPassword(x.target.value)} />
        <button onClick={handleRegister}>Register</button>
    </div>
}