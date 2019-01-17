<?xml version='1.0'?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:formatHelper="urn:xsltExtension-FormatHelper" >

  <xsl:output method="html"/>

  <!-- Get config.file -->


  <xsl:template match="/">

    <html>
      <head>
      </head>
      <body>

        <div id="print-section-has" style="font-family: Arial, Helvetica, sans-serif;color: #000;font-size: 12px;">
          <table align="center" bgcolor="#ffffff" cellpadding="0" cellspacing="0" style="font-family: Arial, Helvetica, sans-serif;color: #000;font-size: 12px;" width="100%">
            <tbody>
              <tr>
                <td align="center">
                  <table bgcolor="#fff" border="0" cellpadding="0" cellspacing="0" width="900">
                    <tbody>
                      <tr>
                        <td align="left" bgcolor="#fff" colspan="2" valign="middle">

                        </td>
                      </tr>
                      <tr>
                        <td align="center" bgcolor="#fff" colspan="2" style="font-size: 20px" valign="top">
                          <b>Aceptación de Servicio  1000090976</b>
                          <br/>
                          <br/>

                        </td>
                      </tr>
                    </tbody>
                  </table>

                  <table bgcolor="#fff" border="0" cellpadding="5" cellspacing="0" class="print-table" style="border:1px solid #999;border-collapse: collapse;" width="900">
                    <tbody>
                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="12" style="border:1px solid #999;font-size: 18px" valign="top">
                          <b>Datos Generales</b>
                        </td>
                      </tr>

                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="6" style="border:1px solid #999;width: 50%" valign="top">
                          <b>Organización Compradora:</b>
                          <br/> INVERSIONES CENTENARIO S.A.A.

                        </td>
                        <td align="left" bgcolor="#fff" colspan="6" style="border:1px solid #999;width: 50%" valign="top">
                          <b>Organización Proveedora:</b>
                          <br/> BURGOS VERGARAY INGENIEROS S.A.C.

                        </td>
                      </tr>

                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="6" style="border:1px solid #999;" valign="top">
                          <b>RUC Organización Compradora:</b>
                          <br/> 20101045995

                        </td>
                        <td align="left" bgcolor="#fff" colspan="6" style="border:1px solid #999;" valign="top">
                          <b>RUC Organización Proveedora:</b>
                          <br/> 20440251677

                        </td>
                      </tr>

                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="3" style="border:1px solid #999;" valign="top">
                          <b>Moneda:</b>
                          <br/>  PEN

                        </td>
                        <td align="left" bgcolor="#fff" colspan="6" style="border:1px solid #999;" valign="top">
                          <b>Número ERP Documento de Material:</b>
                          <br/> 5000097765

                        </td>
                        <td align="left" bgcolor="#fff" colspan="3" style="border:1px solid #999;" valign="top">
                          <b>Estado:</b>
                          <br/>  Aceptada

                        </td>
                      </tr>
                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="3" style="border:1px solid #999;" valign="top">
                          <b>Fecha de Emisión:</b>
                          <br/> 04/10/2018

                        </td>
                        <td align="left" bgcolor="#fff" colspan="6" style="border:1px solid #999;" valign="top">
                          <b>Correo del Proveedor:</b>
                          <br/> test@ebizlatin.com

                        </td>
                        <td align="left" bgcolor="#fff" colspan="3" style="border:1px solid #999;" valign="top">

                        </td>
                      </tr>
                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="12" style="border:1px solid #999;font-size: 18px" valign="top">
                          <b>Datos de la Aceptación</b>
                        </td>
                      </tr>
                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="4" style="border:1px solid #999;" valign="top">
                          <b>Aceptado por:</b>
                          <br/>

                        </td>
                        <td align="left" bgcolor="#fff" colspan="4" style="border:1px solid #999;" valign="top">
                          <b>Autorizado por:</b>
                          <br/>

                        </td>
                        <td align="left" bgcolor="#fff" colspan="4" style="border:1px solid #999;" valign="top">
                          <b>Fecha de la Aceptación:</b>
                          <br/> 04/10/2018

                        </td>
                      </tr>

                      <tr style="border:1px solid #999;">
                        <td align="left" bgcolor="#fff" colspan="12" style="border:1px solid #999;font-size: 18px" valign="top">
                          <b>Detalle de Servicios</b>
                        </td>
                      </tr>
                      <tr style="border:1px solid #999;">
                        <td align="center" bgcolor="#fff" style="border:1px solid #999;" valign="top">
                          <b>No. Item</b>
                        </td>
                        <td align="center" bgcolor="#fff" style="border:1px solid #999;" valign="top">
                          <b>No. Orden de Servicio</b>
                        </td>
                        <td align="center" bgcolor="#fff" style="border:1px solid #999;" valign="top">
                          <b>No. Item Orden de Servicio</b>
                        </td>
                        <td align="center" bgcolor="#fff" colspan="3" style="border:1px solid #999;" valign="top">
                          <b>Descripción del Servicio</b>
                        </td>
                        <td align="center" bgcolor="#fff" colspan="1" style="border:1px solid #999;" valign="top">
                          <b>Estado</b>
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          <b>Cantidad</b>
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          <b>Unidad</b>
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          <b>Valor Recibido</b>
                        </td>
                      </tr>

                      <!--bindings={
  "ng-reflect-ng-for-of": "[object Object]"
}-->
                      <tr style="border:1px solid #999;">
                        <td align="center" bgcolor="#fff" style="border:1px solid #999;" valign="top">
                          1
                        </td>
                        <td align="center" bgcolor="#fff" style="border:1px solid #999;" valign="top">
                          4590024778
                        </td>
                        <td align="center" bgcolor="#fff" style="border:1px solid #999;" valign="top">
                          10.10
                        </td>
                        <td align="center" bgcolor="#fff" colspan="3" style="border:1px solid #999;" valign="top">
                          SRV OBRA CANAL
                        </td>
                        <td align="center" bgcolor="#fff" colspan="1" style="border:1px solid #999;" valign="top">
                          Aceptada
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          1.0000
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          SRV
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          80,369.9900
                        </td>
                      </tr>
                      <tr style="border:0px solid #999;">
                        <td align="center" bgcolor="#fff" colspan="9" style="border:0px solid #999;" valign="top">
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          <b>Total:</b>
                        </td>
                        <td align="center" bgcolor="#fff" colspan="2" style="border:1px solid #999;" valign="top">
                          <b>80,369.9900</b>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </td>
              </tr>
            </tbody>
          </table>
          <br/>
		<p>@imagen</p>
        </div>
      </body>
    </html>

  </xsl:template>

</xsl:stylesheet>