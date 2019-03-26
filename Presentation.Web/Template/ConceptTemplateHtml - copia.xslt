<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:formatHelper="urn:xsltExtension-FormatHelper">

  <xsl:output method="html" />

  <!-- Get config.file -->


  <xsl:template match="/">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"></meta>
        <meta http-equiv="X-UA-Compatible" content="IE=8"></meta>
        <style>
          table table, table table td, table table th {
          border: 1px solid;
          }


        </style>
      </head>
      <body style="margin-top: 0px;margin-left: 0px;">
        <div align="center" >
          <table  cellpadding="0" cellspacing="0" width="776px" height="1016px">
            <tbody style="vertical-align:top;" >
              <tr>
                <td style="padding-left:40px;padding-right:40px;" >
                  <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
                    <tbody>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Consecutivo</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@concept_id"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250" >
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Fecha de Presentación</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@fecha_presentacion_txt"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Nombre investigador</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@investigador"/>

                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Cedula</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@cedula"/>

                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Institución vinculada SNIES</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@institucion"/>

                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Grupo vinculado</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@grupo_vinculado"/>

                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >Fecha de elaboración</div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@fecha_elaboracion_txt"/>

                        </td>
                      </tr>
                    </tbody>
                  </table>
                  <table width="100%"  cellpadding="0" cellspacing="0" style="margin-top:30px;border-collapse: collapse;" >
                    <tbody>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            Título del proyecto de Ley
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@draft_law_title"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            Número
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@draft_law_id"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            Origen
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@draf_law_origen"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            F-presentado
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@draf_law_fecha_presentacion_txt"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            Comisión
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@draf_law_commission"/>
                        </td>
                      </tr>
                      <tr>
                        <td width="250">
                          <div class="p1 ft1" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            Tema
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@draf_law_interested_area"/>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                  <table width="100%" cellpadding="0" cellspacing="0" style="margin-top:30px;border-collapse: collapse;" >
                    <tbody>
                      <tr>
                        <td>
                          <div class="p5 ft2" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:19px;font-family:'Calibri';line-height:23px;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:30px;margin-right:0px;margin-left:0px;" >
                            Resumen del concepto:<br/>
                            <xsl:value-of select="//@summary"/>
                          </div>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                  <table width="100%"  cellpadding="0" cellspacing="0" style="margin-top:30px;border-collapse: collapse;" >
                    <tbody>
                      <tr>
                        <td>
                          <div class="p5 ft2" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:19px;font-family:'Calibri';line-height:23px;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:10px;margin-left:0px;" >
                            Contenido concepto técnico:                          
                          </div>
                          <div class="p5 ft2" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:19px;font-family:'Calibri';line-height:23px;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:10px;margin-left:0px;" >                           
                            @concept                          
                          </div>
                          <div class="p5 ft2" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:19px;font-family:'Calibri';line-height:23px;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:10px;margin-left:0px;" >
                            Bibliografía:                            
                          </div>
                          <div class="p5 ft2" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:19px;font-family:'Calibri';line-height:23px;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:10px;margin-left:0px;" >                            
                            <xsl:value-of select="//@bibliography"/>
                          </div>
                        </td>
                      </tr>
                    </tbody>
                  </table>

                  <table width="100%"  cellpadding="0" cellspacing="0" style="margin-top:30px;border-collapse: collapse;" >
                    <tbody>
                      <tr>
                        <td width="250">
                          <div class="p8 ft2" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:19px;font-family:'Calibri';line-height:23px;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;" >
                            Fecha del certificación del insumo por parte del CAEL
                          </div>
                        </td>
                        <td width="546" style="font-style:normal;font-variant:normal;font-weight:normal;font-size:16px;font-family:'Calibri';line-height:normal;text-align:left;padding-left:10px;margin-top:0px;margin-bottom:0px;margin-right:0px;margin-left:0px;">
                          <xsl:value-of select="//@fecha_elaboracion_txt"/>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
