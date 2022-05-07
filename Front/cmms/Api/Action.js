
const base={
   //baseURL:'http://192.168.1.160:5000'
   baseURL:'https://propupcmms.azurewebsites.net'
    ,headers:{'Content-Type':'application/json','appVersion':'1.0'}}

    const get={method:'get'}
    const post={method:'post'}

    const postForm={method:'post',headers:{'Content-Type':'multipart/form-data'}}
    const postDownload={method:'post',headers:{'responseType':'blob','Accept': 'application/pdf'}}
    

       
    export const doSignin = (data) => ({ url: '/api/signin',...base, ...post,data})
    export const doRegister = (data) => ({ url: '/api/register',...base, ...post,data})

