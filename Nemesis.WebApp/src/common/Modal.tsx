import {Paper} from "./Paper";
import React, {PropsWithChildren} from "react";

const backgroundStyle: React.CSSProperties = {
    background: "rgba(0.5, 0.5, 0.5, 0.5)",
    zIndex: 1000,
    position: "fixed",
    left: 0,
    top: 0,
    width: '100vw',
    height: '100vh  ',
}

const modalStyle: React.CSSProperties = {
    minWidth: "400px",
    position: "absolute",
    top: "50%",
    left: "50%",
    marginRight: "50%",
    transform: "translate(-50%, -50%)"
}

const modalContentStyle: React.CSSProperties = {
    marginTop: "16px",
    marginBottom: "16px"
}

interface ModalProps {
    visible?: boolean,
    onClose?(): void,
    title?: string,
    okButton?: OkButton
}

interface OkButton {
    title?: string,
    onClick(): void,
    disabled?: boolean
}

export const Modal: React.FC<PropsWithChildren<ModalProps>> = ({children, visible, onClose, title, okButton}) => {

    if (!visible)
        return null;

    return <div style={backgroundStyle}>
        <div style={modalStyle  }>
            <Paper>
                {title && <><h3>{title}</h3><hr/></>}
                <div style={modalContentStyle}>{children}</div>
                {(onClose || okButton) && <>
                    <hr/>
                    {okButton && <button disabled={okButton.disabled} onClick={okButton.onClick}>{okButton.title || "Ok"}</button>}
                    {onClose && <button onClick={onClose}>Close</button>}
                </>}
            </Paper>
        </div>
    </div>
}