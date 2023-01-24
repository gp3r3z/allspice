import Axios from 'axios'
import { baseURL } from '../env'

// NOTE api is also being usd to associated the token (see authservice)
export const api = Axios.create({
  baseURL,
  timeout: 8000
})

export const cSharpBE = Axios.create({
  baseURL: 'https://localhost:7045/',
  timeout: 8000
})
