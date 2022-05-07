import BouncyCheckbox from "react-native-bouncy-checkbox";
import React from 'react'
import tw from '../Lib/tailwind'
export function CheckBox ({text,className})
{
    return (
        <BouncyCheckbox style={tw`${className}`}
  size={22}
  fillColor="#FFD615"
  unfillColor="#FFFFFF"
  text={text}
  iconStyle={{ borderColor: "black",borderRadius:0 }}
  textStyle={{color:'black', fontFamily: "Poppins-Regular" }}
  onPress={(isChecked) => {}}
/>
    )
}