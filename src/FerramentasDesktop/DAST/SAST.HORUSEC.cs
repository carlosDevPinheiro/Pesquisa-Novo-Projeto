using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerramentasDesktop.DAST
{
    internal class SAST
    {
        /// 1 - Instalar WSL 2
        /// https://docs.microsoft.com/en-us/windows/wsl/install-manual

        /// 2 - instalar docker for desktop 
        /// https://docs.microsoft.com/pt-br/windows/wsl/tutorials/wsl-containers

        /// 3 - Site Ferramenta
        /// https://github.com/ZupIT/horusec
        /// https://docs.horusec.io/docs/

        //  4 - subir container com ferramenta (docker ubuntu)
        /// docker run -v /var/run/docker.sock:/var/run/docker.sock -v $(pwd):/src horuszup/horusec-cli:latest
       
        /// 5 - executar a ferrMENTA
        /// horusec start -p="DIRETORIO DO PROJETO DENTRO DO UBUNTU" -a="numero do token se houver"
        
        /// INSTAÇÃO DO PAINEL ADMINISTRADOR (UBUNTU)
        /// 1 - git clone https://github.com/ZupIT/horusec-platform.git
        /// 2 - cd horusec-platform
        /// 3 - make install
        /// 4 - http://localhost:8043
        /// 5 - usuario: dev@example.com, senha: Devpass0*
        /// 6 - criar repositorio e token


    }
}
