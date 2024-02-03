import axios from "axios"


export const Endpoints = {
    'trip': {
        'trips': '/trip/list'
    }
}

export default axios.create({
    baseURL: 'https://localhost:7230/api'
})