import React from "react";
import {Route, Routes as InnerRoutes} from "react-router";
import {LobbyPage} from "./lobbies/LobbyPage/LobbyPage";
import {Main} from "./Main";

export const Routes: React.FC = () => {

    return <InnerRoutes>
        <Route path="/lobby/:id" element={<LobbyPage />} />
        <Route path="*" element={<Main />} />
    </InnerRoutes>
}