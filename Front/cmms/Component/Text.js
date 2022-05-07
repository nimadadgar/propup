import React from 'react'
import {Text as OriginalText} from 'react-native'
import tw from '../Lib/tailwind';

export const Text =({children,className, weight,...rest})=>{
   // const _style={fontName:'_iransans_regular',fontWeight:'bold',...style}

  //  style.fontName=style.fontWeight=='normal'?'_iransans_reqular':'_iransans_medium';
   // delete _style.customFontWeight;


   let family=weight===undefined?'Poppins-Regular':`Poppins-${weight}`;



  return  <OriginalText style={[{fontFamily:family}, tw`text-black	 ${className}`]} {...rest}>{children}</OriginalText>
//    return <OriginalText style={{fontFamily:FontWeights(_fontWeight),..._style}} {...rest}>{children}</OriginalText>

}

