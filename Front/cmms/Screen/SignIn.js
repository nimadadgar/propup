
import React from 'react'
import {Button,CircleEffect,Text,Section,Input} from '../Component'
import {useApi,doSignin} from '../Api'
import {
    ScrollView,
   Image,KeyboardAvoidingView,TouchableOpacity,
  } from 'react-native';

export function SignIn({navigation})
{
  const [loading,callApi]=useApi();


  function loadUserData()
  {
    console.log("Not Now")
  }

  async function postForm()
  {
    var result=await callApi( doSignin({email:'',password:'100'}));
console.log(result);
  }
return (

<Section className="flex-1 bg-background_primary ">


    <KeyboardAvoidingView
     style={{flex:1 }}
     behavior={Platform.OS === 'ios' ? 'position' : null}
     keyboardVerticalOffset={Platform.OS === 'ios' ? 50 : 70}
   >
    <ScrollView >
    <CircleEffect />

    <Section className="pt-50% items-center justify-center text-center px-6">
    <Text className="text-xl text-black pb-4" weight="Bold" >Welcome Back</Text>
<Image  source={require('../Assets/pic.png')} />
    </Section>

    <Section className="items-center justify-center text-center pt-4 px-6">

    <Section className=" w-full"> 
      <Input  placeholder="Enter your full name" placeholderTextColor="#000"  ></Input>
      </Section>

      <Section className="pt-3 w-full"> 
      <Input placeholder="Confirm Password" placeholderTextColor="#000"  ></Input>
      </Section>

      <Section className="pt-2"> 
     <Text weight="Bold" className="text-lg text-btn_primary_color">Forget Password</Text>
      </Section>
      
      <Section className="w-full "> 
      <Button title="Sign In" onPress={()=>postForm()} isLoading={loading} disabled={loading} className=" mt-4" fontWeight="Bold" ></Button>
      </Section>
      <Section className="items-center flex flex-row  justify-center pt-2"> 
    
     <Text weight="Bold" className="text-sm pr-2">
       Don’t have an account? 
       </Text>

       <TouchableOpacity   onPress={()=>navigation.navigate("SignUp")}  activeOpacity={0.5} >
       <Text  weight="Bold" className="text-lg  text-btn_primary_color"> 
         Sign Up
       </Text>
       </TouchableOpacity>
       
      </Section>

      </Section>

      </ScrollView>
  </KeyboardAvoidingView>

</Section>

)
}
