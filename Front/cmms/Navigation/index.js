import React from 'react';
import tw from '../Lib/tailwind'
import { NavigationContainer,useNavigationContainerRef  } from '@react-navigation/native';
import {Splash} from '../Screen/Splash'
import {SignUp} from '../Screen/SignUp'
import {SignIn} from '../Screen/SignIn'
import {Home} from '../Screen/Home'

import { createNativeStackNavigator } from '@react-navigation/native-stack';


const MainStack = createNativeStackNavigator();
export const MainNavigation = () => (
    <MainStack.Navigator initialRouteName="Splash"
    screenOptions={
        {gestureDirection: 'horizontal',
            gestureEnabled: true,
            headerShown: false
        }
    }    
    >            
        <MainStack.Screen name="Splash" component={Splash} />

    <MainStack.Screen name="SignIn" component={SignIn} />
   <MainStack.Screen name="SignUp" component={SignUp} />
    <MainStack.Screen name="Home" component={Home} />

    </MainStack.Navigator>
);
