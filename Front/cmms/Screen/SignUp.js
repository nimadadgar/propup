
import React from 'react'
import tw from '../Lib/tailwind'
import {Button,CircleEffect,Text,Section,Input} from '../Component'
import { useFormFields } from '../Lib/HookForm';
import {
    ScrollView,
   KeyboardAvoidingView
  } from 'react-native';

export function SignUp()
{

  const [values, handleValuesChange] = useFormFields({
    fullName:"",
    email: "",
    password: "",
    company:"",
    confirmPassword:"",

  
  });

  function submit()
  {
    console.log(values);
  }

return (
<Section className="flex-1 bg-background_primary  px-6">
    <CircleEffect />
<KeyboardAvoidingView
     style={{flex:1 }}
     behavior={Platform.OS === 'ios' ? 'position' : null}
     keyboardVerticalOffset={Platform.OS === 'ios' ? 50 : 70}
   >
    <ScrollView>
 <Section className="pt-50% items-center justify-center text-center">
<Text className="text-xl text-black " weight="Medium" >Welcome to OnBoard</Text>
    <Text className="text-sm text-black py-3" >We help you meet up you tasks on time.</Text>
    </Section>
    <Section className="py-2 w-full"> 
      <Input
         value={values.fullName}
         onChange={handleValuesChange}
         id="fullName"
      placeholder="Enter your full name" 
      placeholderTextColor="#000"  >

      </Input>
      </Section>


      <Section className="py-2 w-full"> 
      <Input placeholder="Enter your email"  placeholderTextColor="#000"  ></Input>
      </Section>
      
      <Section className="py-2 w-full"> 
      <Input
      placeholder="Enter password" placeholderTextColor="#000"  ></Input>
      </Section>
      
      <Section className="py-2 w-full"> 
      <Input placeholder="Confirm password" placeholderTextColor="#000"  ></Input>
      </Section>
      <Section className="py-2 w-full"> 
      <Input placeholder="Company" placeholderTextColor="#000"  ></Input>
      </Section>

      
      <Section className="py-5 w-full"> 
      <Button title="Get Started" onPress={()=>submit()} className=" mt-10" fontWeight="Bold" ></Button>
      </Section>



    </ScrollView>
  </KeyboardAvoidingView>

</Section>
)
}
