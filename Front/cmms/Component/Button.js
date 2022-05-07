import React from 'react';
import {
    SafeAreaView,
    ScrollView,
    StatusBar,
    StyleSheet,
    ActivityIndicator,
    TouchableOpacity,
        useColorScheme,
    View,
  } from 'react-native';
import  {Text} from './Text'

import tw from '../Lib/tailwind';


export function Button({ children,fontWeight, onPress,isLoading, title, className,disabled }) {
  return (
    <TouchableOpacity  onPress={onPress} disabled= {disabled?'disabled':''} activeOpacity={0.5}  
    style={[styles.shadow, tw` h-12 w-full items-center justify-center rounded-lg  flex h-20 ${disabled?'bg-btn_primary_disablecolor':'bg-btn_primary_color'} ${className}`]}>  
      {children}

      {!isLoading && title && 
        <Text weight={fontWeight} className='text-black text-center   text-2xl'>{title}</Text>
      }
  {isLoading && <ActivityIndicator size="large" color="#FFD615" /> }



    </TouchableOpacity>
  )
}


const styles = StyleSheet.create({


  shadow: {  
    borderWidth:0,
    overflow: 'hidden',
    elevation: 8,
    borderRadius:15,
    shadowColor: 'gray',
    shadowRadius: 2,  
    shadowRadius: 50,
    shadowOpacity:0,
  },
  
  
});


