import React, {useState} from "react";
import {Input} from "../common/Input";
import {Select} from "../common/Select";
import {createLobby} from "../api/lobbiesApi";
import {useNavigate} from "react-router";


export const NewLobbyForm: React.FC = () => {

    const [title, setTitle] = useState("");

    const isReady = title !== "";

    const navigate = useNavigate();

    function handleCreate() {
        createLobby({
            title: title
        })
            .then(response => navigate(`/lobby/${response.id}`))
    }

    return <div>
        <p>Title</p>
        <Input value={title} onChange={setTitle} />
        {/*<p>Password</p>*/}
        {/*<Input disabled/>*/}
        {/*<p>Mode</p>*/}
        {/*<Select />*/}
        <hr/>
        <button disabled={!isReady} onClick={handleCreate}>Create</button>
    </div>
}