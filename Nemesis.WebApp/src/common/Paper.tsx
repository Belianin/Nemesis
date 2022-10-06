import React, {PropsWithChildren} from "react";

const paperStyle = {
    borderRadius: "8px",
    padding: "16px 16px 16px 16px",
    background: "white",
    boxShadow: "2px 2px 2px rgba(0.5, 0.5, 0.5, 0.1)"
}

export const Paper: React.FC<PropsWithChildren> = ({children}) => {

    return <div style={paperStyle}>
        {children}
    </div>
}