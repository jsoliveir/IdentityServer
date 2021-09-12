import jwt_decode from "jwt-decode"
import { Cookies } from 'react-cookie'
import { useEffect,useState } from "react";

const cookies = new Cookies();

function getAccessTokenFromUrl(){
    var matches = window.location.href.match(/#access_token=([^&]*)/);
    return matches && matches[1]
}

export function getAccessToken(){
    var access_token = getAccessTokenFromUrl()   
    return access_token || cookies.get("access_token")
}
export function setAccessToken(token,expire){
    cookies.set("access_token",token,{path:"/",expires: new Date(Date.now() + (expire * 1000))});
}

export default function Authorize({authority,scopes,children,expire=30}) {
    const [isAuthorized, setAuthorized] = useState(false)
    useEffect(()=>{
        if(!isAuthorized){
            document.forms["authorize"].submit()
        }
    },[isAuthorized])
    try{
        var access_token = getAccessToken();
        if(access_token){
            setAccessToken(access_token,expire);
        }
        if(getAccessTokenFromUrl()){
            window.history.pushState("", document.title, window.location.pathname);  
        }
        var jwt = jwt_decode(access_token);
        if(!jwt.exp > (new Date().getTime() / 1000)){
            throw "Token is expired"
        }
        if(!jwt.iss.match(authority)){
            throw "Token is invalid"
        }
        if(!isAuthorized){
            setAuthorized(true);
        }
    }catch (ex){

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
