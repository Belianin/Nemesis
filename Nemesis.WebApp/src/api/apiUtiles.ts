export function get(url: string): Promise<Response> {
    return sendRequest("GET", url);
}

export function post(url: string, body?: any): Promise<Response> {
    return sendRequest("POST", url, body);
}

export function sendRequest(method: "GET" | "POST" | "PUT" | "DELETE" | "PATCH", url: string, body?: any): Promise<Response> {

    const headers: HeadersInit = {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
    
    const sid = localStorage.getItem("sid");
    if (sid !== null)
        headers["Authorization"] = sid;

    return fetch(url, {
        method: method,
        body: body ? JSON.stringify(body) : body,
        credentials: "include",
        headers: headers
    })
}