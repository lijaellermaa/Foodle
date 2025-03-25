import type {Config} from 'tailwindcss'
import tailwindTypography from "@tailwindcss/typography"
import daisyui from "daisyui";

export default {
  content: ['./src/**/*.{html,js,ts}'],
  theme: {
    extend: {},
  },
  plugins: [tailwindTypography, daisyui],
  daisyui: {
    themes: ["bumblebee", "synthwave", {
      mytheme: {
        accent: "#0284c7"
      }
    }]
  }
} satisfies Config

