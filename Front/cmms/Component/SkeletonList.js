import React from 'react'
import {
    ScrollView,
    View,
   Image,KeyboardAvoidingView
  } from 'react-native';
  import SkeletonPlaceholder from "react-native-skeleton-placeholder";

export function SkeletonList()
{
  return (
    <SkeletonPlaceholder>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    <View style={{marginTop:10, flexDirection: "row", alignItems: "center" }}>
      <View style={{ width: 25, height: 25, borderRadius: 5}} />
      <View style={{marginLeft: 20,flex:1, width: 38000, height: 20 }} />
    </View>
    

  </SkeletonPlaceholder>

  )
}