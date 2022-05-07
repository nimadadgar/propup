
import React from 'react'

import {Button,CircleEffect,Text,Section} from '../Component'
import {
  Image,
} from 'react-native';
export function Splash({navigation })
{
console.log(navigation)
return (
 
  <Section className='bg-background_primary flex-1  items-center justify-center'>
  <CircleEffect />
    <Section className='items-center justify-center pt-40% px-2'>
    <Image  source={require('../Assets/pic.png')} />
<Section className='items-center justify-center'>

<Text className="text-xl text-black pt-10" weight="Medium" >Let’s get things done on time</Text>

<Text className="text-center text-sm  mx-5 leading-5 pt-4	 " weight="Regular">
Utilising simple but efficient maintenance modules, we can Capture comprehensive asset information, manage day to day maintenance tasks, schedule preventative maintenance and inspections, handle automatic stores ordering and produce detailed statutory reports.
</Text>

</Section>
    </Section>
    <Button title="Get Started" onPress={()=>
        navigation.reset({
          index: 0,
          routes: [{ name: "SignIn" }],
        })
    
    } className="w-4/5 mt-10" fontWeight="Bold" ></Button>
  </Section>
 


)
}
