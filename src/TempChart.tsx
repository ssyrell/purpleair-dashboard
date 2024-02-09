import { LineChart } from "./LineChart";
import { ChartData } from "./ChartData";

function mapValues(data: any[]): ChartData {
  const formatter = new Intl.DateTimeFormat(undefined, { timeStyle: "short" });
  const labels: string[] = [];
  const values: number[] = [];

  data.forEach((row: any) => {
    labels.push(formatter.format(Date.parse(row.timestamp)));
    values.push(row.tempFahrenheit);
  });

  return { labels, values };
}

export interface TempChartProps {
  data: any[];
}

export function TempChart(props: TempChartProps) {
  const mappedData = mapValues(props.data);
  return (
    <LineChart
      title={"Temperature"}
      borderColor={"rgb(255, 99, 132"}
      backgroundColor={"rgba(255, 99, 132, 0.5"}
      data={mappedData}
    />
  );
}
