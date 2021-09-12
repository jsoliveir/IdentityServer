import jwt_decode from "jwt-decode"
import { useEffect,useState } from "react";

function getAccessTokenFromUrl(location){
    var matches = location.match(/#access_token=([^&]*)/);
    return matches && matches[1]
}

export function getAccessToken(){
    var access_token = getAccessTokenFromUrl(window.location.href)
    if(access_token){
        localStorage.setItem("session",access_token);
        window.history.pushState("", document.title, window.location.pathname);  
    }
    return localStorage.getItem("session")
}

export default function Authorize({authority,scopes,children}) {
    useEffect(()=>{
        if(!getAccessToken()){
            document.forms["authorize"].submit()
        }
    })
    try{
        var access_token = jwt_decode(getAccessToken());
        var is_token_valid = 
             access_token.iss.match(authority) &&
            access_token.exp > (new Date().getTime() / 1000) 
        if(!is_token_valid) {
            throw "Token expired"
        }
    }catch (ex){
        console.error(ex)        
        return (
            <form id="authorize" method="POST" action={authority + "/connect/authorize"} >
                <input type="hidden" name="client_id" value="client"/>
                <input type="hidden" name="scope" value={scopes}/>
                <input type="hidden" name="response_type" value="token"/>
                <input type="hidden" name="redirect_uri" value={window.location.origin}/>
            </form>
        )
    }

    return children
}
