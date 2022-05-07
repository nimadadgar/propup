import create from 'zustand'
import { persist } from "zustand/middleware"



export const useStore   = create(persist(
  (set, get) => ({
    user:null,
    setUser: (userToken) => set(state => ({ user: userToken })),
    removeUser: () => set(state => ({ user:null })),
  }),
  {
    name: "cmms-storage", // unique name
    getStorage: () => localStorage, // (optional) by default, 'localStorage' is used
  }
))