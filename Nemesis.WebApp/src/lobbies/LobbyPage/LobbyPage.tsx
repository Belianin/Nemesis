import React, { useEffect, useRef, useState } from "react";
import {useParams} from "react-router";
import { Input } from "../../common/Input";

interface ChatMessage {
    author: string,
    message: string
}

export const LobbyPage: React.FC = () => {

    const {id} = useParams();

    const socket = useRef<WebSocket|null>(null);
    const [messages, setMessages] = useState<ChatMessage[]>([])

    useEffect(() => {
        if (!id)
            return;

        socket.current = new WebSocket(`ws://localhost:5000/v1/lobbies/${id}/connect?sid=${localStorage.getItem("sid")}`);
        socket.current.onmessage = handleMessage

        const current = socket.current;

        //return () => current.close()
    }, [id])

    function handleMessage(e: MessageEvent<any>) {
        console.log(e.data);

        if (typeof e.data === 'string') {
            const data = JSON.parse(e.data);

            if (data.type === "Message") {
                setMessages(prev => [...prev, {author: data.author, message: data.message}])
            }
        }
    }

    function sendMessage(message: string) {

        if (!socket.current)
            return;

        socket.current!.send(JSON.stringify({type: "Message", message: message}))
    }

    return <div>
        <p>Lobby: {id}</p>
        {<Chat messages={messages} sendMessage={sendMessage} />}
        </div>
}

interface ChatProps {
    sendMessage(message: string): void,
    messages: ChatMessage[]
}

const Chat: React.FC<ChatProps> = ({sendMessage, messages}) => {

    const [message, setMessage] = useState<string>("");

    function handleMessage() {
        sendMessage(message);
        setMessage("");
    }

    return <>
        <div>{messages.map((x, i) => <p key={i}>{x.author}: {x.message}</p>)}</div>
        <Input value={message} onChange={setMessage} />
        <button onClick={handleMessage}>SendMessage</button>
    </>

}