/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import React from 'react';
import tw from './Lib/tailwind'
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationContainer,useNavigationContainerRef  } from '@react-navigation/native';
import {Splash} from './Screen/Splash'
import {SignUp} from './Screen/SignUp'
import {MainNavigation} from './Navigation'

import SkeletonPlaceholder from "react-native-skeleton-placeholder";

import {
  ScrollView,
  Image,
  View,StyleSheet,useColorScheme,SafeAreaView,TextInput,KeyboardAvoidingView
} from 'react-native';

import {Button,Text,CircleEffect,Input,Section} from './Component'

const Stack = createNativeStackNavigator();

function HomeScreen({ navigation }) {
  return (
    <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
      <Text>Home Screen</Text>
      <Button
        title="Go to Details"
        onPress={() => navigation.navigate('Details')}
      />
    </View>
  );
}

function DetailsScreen() {
  return (
    <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
      <Text>Details Screen</Text>
    </View>
  );
}



const App= () => {
  const navigationRef = useNavigationContainerRef(); // You can also use a regular ref with `React.useRef()`

  return (
     <NavigationContainer>
    <MainNavigation />
     </NavigationContainer>
  
  );
};




export default App;
