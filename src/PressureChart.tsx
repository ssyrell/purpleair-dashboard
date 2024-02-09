import { LineChart } from "./LineChart";
import { ChartData } from "./ChartData";

function mapValues(data: any[]): ChartData {
  const formatter = new Intl.DateTimeFormat(undefined, { timeStyle: "short" });
  const labels: string[] = [];
  const values: number[] = [];

  data.forEach((row: any) => {
    labels.push(formatter.format(Date.parse(row.timestamp)));
    values.push(row.pressure);
  });

  return { labels, values };
}

export interface PressureChartProps {
  data: any[];
}

export function PressureChart(props: PressureChartProps) {
  const mappedData = mapValues(props.data);
  return (
    <LineChart
      title={"Pressure"}
      borderColor={"rgb(99, 255, 132"}
      backgroundColor={"rgba(99, 255, 132, 0.5"}
      data={mappedData}
    />
  );
}
