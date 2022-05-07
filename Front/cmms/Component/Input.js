import React from 'react'
import {TextInput} from 'react-native'
import tw from '../Lib/tailwind'


export const Input =({placeholder,id,value, className,onChange, weight,...props})=>{

    let family=weight===undefined?'Poppins-Regular':`Poppins-${weight}`;
    return (
    <TextInput placeholder={placeholder} 
    {...props}
    value={value}
    id={id}
    onChange={onChange}
    placeholderTextColor="#000" style={[{fontFamily:family}, tw`bg-white rounded-3xl px-5 py-4 text-black 	 w-full  `]}  ></TextInput>

    )

}
