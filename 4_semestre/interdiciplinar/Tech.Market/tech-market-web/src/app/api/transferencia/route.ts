import axios, { AxiosError } from "axios"
import https from "https";



export async function POST(req: Request) {
    try {
        const body = await req.json()
        console.log(process.env.URL)
        return await axios.post(process.env.URL + "/api/Transacoes", body, {
            httpsAgent: new https.Agent({
                rejectUnauthorized: false
            })
        }).then(() => {
            return Response.json({}, { status: 201 })
        })
            .catch((err) => {
                const res: { message: string } = {} as { message: string }

                if (err instanceof AxiosError) {
                    res.message = err.response?.data.message ?? "Erro desconhecido";
                } else
                    res.message = "Erro desconhecido";

                return Response.json(res, { status: 400 })
            })
    }
    catch (e) {
        console.log(e)
    }
}