import React from 'react'
import { useState } from "react";

export function useFormFields(initialState) {
  const [fields, setValues] = useState(initialState);

  return [
    fields,
    function(event) {
        console.log(event.target.id)
      setValues({
        ...fields,
        [event.target.id]: event.target.value
      });
    }
  ];
}