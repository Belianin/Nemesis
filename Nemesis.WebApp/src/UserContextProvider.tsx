import React, { PropsWithChildren, useContext, useEffect, useState } from 'react'
import { getMe, User } from './api/usersApi'

interface UserContextProps {
    user?: User,
    handleSession(sessionId: string): Promise<void>
}

const UserContext = React.createContext<UserContextProps>({} as UserContextProps)

export const useUser = () => useContext(UserContext);

export const UserContextProvider: React.FC<PropsWithChildren> = ({children}) => {

    const [user, setUser] = useState<User>();

    useEffect(() => {
        if (localStorage.getItem("sid"))
            fetchUser()
    }, [])

    function fetchUser() {
        getMe().then(setUser, () => console.log(1));
    }

    function handleSession(sessionId: string): Promise<void> {
        localStorage.setItem("sid", sessionId);
        return getMe().then(setUser, x => new Promise(() => {}));
    }

    const context = {
        user,
        handleSession
    }

    return <UserContext.Provider value={context}>
        {children}
    </UserContext.Provider>
}