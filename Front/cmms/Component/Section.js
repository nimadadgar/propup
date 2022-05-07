import React from 'react'
import tw from '../Lib/tailwind';
import {View} from 'react-native'

export const Section=({children,className,styles})=>{
    return <View style={[{...styles}, tw`text-black	 ${className}`]} >{children}</View>
}