import { useState, useEffect } from "react";
import { TempChart } from "./TempChart";
import { PressureChart } from "./PressureChart";

function App() {
  const [data, setData] = useState([]);

  useEffect(() => {
    (async function () {
      const responseData = await (
        await fetch("/api/sensorData/a4:e5:7c:28:a7:c/0/100")
      ).json();
      setData(responseData);
    })();
  }, []);

  return (
    <div>
      <TempChart data={data} />
      <PressureChart data={data} />
    </div>
  );
}

export default App;
