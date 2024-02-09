import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Line } from "react-chartjs-2";
import { ChartData } from "./ChartData";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

export const options = {
  responsive: true,
  plugins: {
    legend: {
      position: "top" as const,
    },
    title: {
      display: true,
      text: "Chart.js Line Chart",
    },
    scales: {
      x: {
        display: true,
      },
      y: {
        suggestedMin: -100,
        suggestedMax: 1000,
      },
    },
  },
};

export interface LineChartProps {
  title: string;
  data: ChartData;
  borderColor: string;
  backgroundColor: string;
}

export function LineChart(props: LineChartProps) {
  const data = {
    labels: props.data.labels,
    datasets: [
      {
        label: props.title,
        data: props.data.values,
        borderColor: props.borderColor,
        backgroundColor: props.backgroundColor,
        cubicInterpolationMode: "monotone",
      },
    ],
  } as any;
  return <Line options={options} data={data} />;
}
