import { Redirect } from "react-router-dom"
import jwt_decode from "jwt-decode"

function get_access_token_from_url(location){
    var matches = location.hash.match(/#access_token=([^&]*)/);
    return matches && matches[1]
}

function get_access_token(location){
    var access_token = get_access_token_from_url(location)
    if(access_token){
        localStorage.setItem("access_token",access_token);
        return access_token
    }
    return localStorage.getItem("access_token");
}

export default function Authorize({login,children,...props}) {
    try{
        var access_token = get_access_token(props.location)
        var decoded = jwt_decode(access_token);
        var is_token_expired = decoded.exp < ((new Date().getTime() + 1) / 1000) 
        if(is_token_expired)
            throw "Token expired"
    }catch (ex){
        console.error(ex)
        return <Redirect to={login} />
    }
    return children
}
