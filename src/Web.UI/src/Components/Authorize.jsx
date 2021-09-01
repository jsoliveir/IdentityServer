import { Redirect } from "react-router-dom"
import { useState } from "react"
import jwt_decode from "jwt-decode"

export default function Authorize({login,token,children}) {
    const [session, setSession] = useState(false)

    if(token){
        setSession(session);
    }else if(!session){
        return <Redirect to={login} />
    }
    return children
}
