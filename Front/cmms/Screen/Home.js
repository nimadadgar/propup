
import React,{useState} from 'react'
import {Button,CircleEffect,Text,Section,Input,CheckBox,SkeletonList} from '../Component'

import {
    ScrollView,
    View,
   Image,KeyboardAvoidingView
  } from 'react-native';


  

export function Home()
{

const [loading,setLoading]=useState(true);


 
return (

<Section className="flex-1 bg-background_primary ">
<Section className="bg-btn_primary_color items-center justify-center pt-20%">
    <CircleEffect color="#FFFCEE78"/>
      <Section className="items-center justify-center w-32 h-32 bg-red-500 rounded-full	 ">
      <Image resizeMode="cover" 
      style={{width:'100%',height:'100%',
      borderRadius:150/2
    }}  source={{
        uri: 'https://picsum.photos/200/300',
      }}
       />
      </Section>
      <Text className="pt-2 pb-5 text-sm" weight='Bold'>Welcome Javad Zobeidi</Text>
    </Section>

    
    
    <Section className='flex-5 pt-4 px-4'>

    <Section  name="workorder_Container" className="bg-white px-4 rounded-2xl shadow-2xl flex-1	">
      {loading && <SkeletonList />}
    {!loading && 
      <ScrollView vertical nestedScrollEnabled ={true} >
      <Text weight='Bold' className="pt-5 text-sm ">Work Orders</Text>
      <CheckBox  className="py-1" text={'WO64332 Reactive maintenance'}/>
      <CheckBox   className="py-1" text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox   className="py-1" text={'WO64332 Reactive maintenance'}/>
      <CheckBox   className="py-1" text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      <CheckBox  className="py-1"  text={'WO64332 Reactive maintenance'}/>
      </ScrollView>
    }
    </Section>


    </Section>  
     

    <Section className='flex-1 bg-white   px-4 rounded-2xl shadow-2xl mx-4 my-4 flex-row items-center justify-center'>
   
    <Section className=" items-center justify-center py-2 flex-1 	">
          <Image  source={require('../Assets/improvement.png')} />
            <Text className="text-xs text-center">Improvement Suggestions</Text>
          </Section>

          <Section className="text-center  flex-col items-center justify-start py-2 flex-1 	">
          <Image  source={require('../Assets/qrcode.png')} />
            <Text className="text-xs text-center">Search Equipments</Text>
          </Section>
          <Section className="flex-col 	 items-center justify-center py-2 flex-1 	">
          <Image  source={require('../Assets/clock.png')} />
            <Text className="text-xs text-center">Availibility</Text>
          </Section>
      </Section>
    

</Section>

)
}

  

