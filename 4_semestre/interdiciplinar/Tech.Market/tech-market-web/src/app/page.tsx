import { HomeServerViewModel } from "@/view-models/home.server.view-model";
import Link from "next/link";
import { BsArrowRight } from "react-icons/bs";
import { NovaContaComponent } from "./(components)/nova-conta.component";

const HomePage = async () => {
  const homeViewModel = new HomeServerViewModel();
  await homeViewModel.getContasAsync();

  return (
    <div className="grid grid-cols-12 gap-4 content-center">
      <div className="col-span-12 md:col-span-6 lg:col-span-6 xl:col-span-6  md:col-start-4  lg:col-start-4 xl:col-start-4 justify-end flex">
        <NovaContaComponent />
      </div>
      <div className="col-span-12 md:col-span-6 lg:col-span-6 xl:col-span-6  md:col-start-4  lg:col-start-4 xl:col-start-4">
        <ul>
          {homeViewModel.contas.map((item) => {
            return (
              <li
                className="bg-white m-1 p-1 rounded-sm shadow relative"
                key={item.id}
              >
                <div>{item.nome}</div>
                <div>{item.cpfMask}</div>
                <Link
                  className="absolute right-0 top-0 p-2"
                  about="Ir para "
                  href={`/transacoes/${item.id}`}
                >
                  <BsArrowRight />
                </Link>
              </li>
            );
          })}
        </ul>
      </div>
    </div>
  );
};

export default HomePage;
