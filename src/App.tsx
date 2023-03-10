import React, { useState, useEffect } from 'react';

function App() {
  const [data, setData] = useState(0);

  useEffect(() => {
    (async function () {

      const responseData = await (await fetch("/api/sensorData/a4:e5:7c:28:a7:c/0/1")).json();
      console.log("Got response data");
      console.log(responseData);
      console.log(`Current temp F is ${responseData[0].currentTempFahrenheit}`);
      setData(responseData[0].currentTempFahrenheit);
    })();
  });

  return <div>The current temperature is {data} F</div>;
}

export default App;
