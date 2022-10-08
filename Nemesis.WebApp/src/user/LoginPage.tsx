import React, { useState } from 'react';
import { useNavigate } from 'react-router';
import { Input } from "../common/Input"
import { useUser } from '../UserContextProvider';
import { LoginForm } from './LoginForm';
import { RegisterForm } from './RegisterForm';

export const LoginPage: React.FC = () => {
    
    const {user} = useUser();

    const navigate = useNavigate()
    if (user)
        navigate("/");

    return <>
        <LoginForm />
        <hr/>
        <RegisterForm />
    </>
}
