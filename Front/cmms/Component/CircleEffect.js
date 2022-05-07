import React from 'react';
import {
    View
  } from 'react-native';
  
export const CircleEffect=({color})=>{
    var cw=176

    let currentColor=color===undefined?"#FFD61578":color;

    return (
        <>
        <View style={{backgroundColor:currentColor,width:cw,height:cw,borderRadius:cw,position:'absolute',top:(cw/2)*-1,left:0}}></View>
        <View style={{backgroundColor:currentColor,width:cw,height:cw,borderRadius:cw,position:'absolute',top:0,left:(cw/2)*-1}}></View>
        </>

    )
}