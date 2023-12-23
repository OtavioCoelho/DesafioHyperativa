﻿using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHyperativa.Test.Mocking;
public static class FormFileMock
{
    public static IFormFile contentCorrect = GetFormFile(@"DESAFIO-HYPERATIVA           20180524LOTE0001000010   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001000010                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorDate = GetFormFile(@"DESAFIO-HYPERATIVA           20186024LOTE0001000010   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001000010                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorCardSmallSize = GetFormFile(@"DESAFIO-HYPERATIVA           20180524LOTE0001000010   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897
C7     4456897
C8     4456897
C9     4456897
C10    4456897
LOTE0001000010                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorDiffLotHeaderFooter = GetFormFile(@"DESAFIO-HYPERATIVA           20180524LOTE0002000010   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001000010                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorDiffQuantityHeaderFooter = GetFormFile(@"DESAFIO-HYPERATIVA           20180524LOTE0001000090   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001000010                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorEmptyNameLot = GetFormFile(@"DESAFIO-HYPERATIVA           20180524        000010   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001000010                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");
    
    public static IFormFile contentErrorQuantityZero = GetFormFile(@"DESAFIO-HYPERATIVA           20180524LOTE0001000000   // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001000000                                        // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorQuantityNotInforming = GetFormFile(@"DESAFIO-HYPERATIVA           20180524LOTE0001         // [01-29]NOME   [30-37]DATA   [38-45]LOTE   [46-51]QTD DE REGISTROS
C2     4456897999999999                               // [01-01]IDENTIFICADOR DA LINHA   [02-07]NUMERAÇÃO NO LOTE   [08-26]NÚMERO DE CARTAO COMPLETO
C1     4456897922969999                               // OBS. ORIENTACAO NÚMERICA A ESQUERDA E ARQUIVO INTEIRO COMPLETADO COM ESPAÇOS ATÉ COLUNA 51
C3     4456897999999999
C4     4456897998199999
C5     4456897999999999124
C6     4456897912999999
C7     445689799999998
C8     4456897919999999
C9     4456897999099999
C10    4456897919999999
LOTE0001                                              // [01-08]LOTE   [09-14]QTD DE REGISTROS

");

    public static IFormFile contentErrorEmpty = GetFormFile(@"");


    private static IFormFile GetFormFile(string Content)
    {
        var fileName = "DESAFIO-HYPERATIVA.txt";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(Content);
        writer.Flush();
        ms.Position = 0;

        var formFile = new Mock<IFormFile>();
        formFile.Setup(f => f.OpenReadStream()).Returns(ms);
        formFile.Setup(f => f.FileName).Returns(fileName);
        formFile.Setup(f => f.Length).Returns(ms.Length);

        return formFile.Object;
    }

}
