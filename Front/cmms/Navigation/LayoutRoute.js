import React,{useEffect,useState} from 'react'
import { useQuery } from 'react-query';
import {LoadingLayout,ErrorLayout} from '../Component'

export function LayoutRoute ({
    apiCall,
    isAllowed,
    redirectPath = '',
    children,
    Component,
  }) {
  
  const [state,setState]=useState({moduleLoadResult:0,props:null});

  
  const { data, isLoading, isSuccess ,
    isError,
    isRefetchError,
    error,
    isFetching,
    refetch,
    isPreviousData,status
} = useQuery('apiCall', apiCall,{   refetchOnWindowFocus: false,enabled:false  });


 function manageApi()
{
  var type= typeof apiCall ;
     if (type=='function')
     {

   refetch().then((result)=>{

    if (result.data.httpCode==200)
    {
     setState({ moduleLoadResult: 1, props: result.data.response })
    }
    else
   {
     setState({ moduleLoadResult: 2, props:result.data.message })
   }

   });


     }


}

    useEffect(()=>{
      manageApi();
 // setState({ moduleLoadResult: 1, props: null })
},[])
  
  return (
  
  <>
  {isFetching && (<LoadingLayout />)   }
  {state.moduleLoadResult===1 && (<Component  props={state.props}  />)   }
  {state.moduleLoadResult=== 2&& (<ErrorLayout error={state.props}  />  )}
  </>
  
  )
  
  };