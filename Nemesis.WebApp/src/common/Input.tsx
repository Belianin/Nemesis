import React from "react";

interface InputProps {
    value?: string,
    onChange?(value: string): void,
    disabled?: boolean
}

export const Input: React.FC<InputProps> = ({value, onChange, disabled}) => {

    function handleValue(e: React.ChangeEvent<HTMLInputElement>) {
        if (onChange)
            onChange(e.target.value);
    }

    return <input disabled={disabled} value={value} onChange={handleValue}/>
}