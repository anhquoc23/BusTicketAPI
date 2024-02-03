import { useEffect, useState } from "react"
import { Alert, Button, Spinner, Table } from "react-bootstrap"
import Api, { Endpoints } from "../../services/Api"


const Trip = () => {
    const [trips, setTrips] = useState(null)


    useEffect(() => {
        const loadTrips = async() => {
            try {
                let result = await Api.get(Endpoints['trip']['trips'])
                setTrips(result.data)
                console.log(trips)
            } catch(err) {
                console.log(err)
            }
        }

        loadTrips()
    }, [])

    if (trips === null) return <Spinner animation="border" />

    return(
        <>
            <Alert variant="info">Danh Sách Chuyến Đi</Alert>
            <Table responsive>
                <thead>
                    <tr>
                        <th>Ngày Khởi Hành</th>
                        <th>Giờ Đi</th>
                        <th>Giá Vé</th>
                        <th>Người Lái Xe</th>
                        <th>Số xe</th>
                        <th>Tuyến đường</th>
                        <th>Khoảng cách</th>
                        <th colSpan={2} className="text-center">Action</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        trips.map(t => (
                                <tr>
                                    <td>{t.tripDate}</td>
                                    <td>{t.startTime}</td>
                                    <td>{t.unitPrice}</td>
                                    <td>{t.driverName}</td>
                                    <td>{t.busNumber}</td>
                                    <td>{t.routine}</td>
                                    <td>{t.distance}</td>
                                    <td><Button variant="warning">Sửa</Button></td>
                                    <td><Button variant="danger">Xoá</Button></td>
                                </tr>
                            )
                        )
                    }
                </tbody>
            </Table>

            
        </>
    )
}

export default Trip