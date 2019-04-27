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
        <table class="table_full editable-bg-color bg_color_e6e6e6 editable-bg-image" bgcolor="#e6e6e6" width="100%" align="center"
  cellspacing="0" cellpadding="0" border="0">
          <tr>
            <td>

              <table class="table1 editable-bg-color bg_color_303f9f" bgcolor="#DDDDDD" width="600" align="center" border="0" cellspacing="0"
                  cellpadding="0" style="margin: 0 auto;">

                <tr>
                  <td height="25"></td>
                </tr>
                <tr>
                  <td>

                    <table class="table1" width="520" align="center" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
                      <tr>
                        <td>

                          <table width="100%" align="left" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td  align="center" class="text_color_282828" style="color: #282828; font-size: 15px; font-weight: 700; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                                <div class="editable-text">
                                  <span class="text_container">
                                    <multiline>
                                      Estimado(a) "<xsl:value-of select="//@name" />":
                                    </multiline>
                                  </span>
                                </div>
                              </td>
                            </tr>
                            <tr>
                              <td height="22"></td>
                            </tr>
                          </table>




                        </td>
                      </tr>



                    </table>

                  </td>
                </tr>


              </table>

            </td>
          </tr>
          <tr>
            <td>

              <table class="table1 editable-bg-color bg_color_303f9f" bgcolor="#fff" width="600" align="center" border="0" cellspacing="0"
                  cellpadding="0" style="margin: 0 auto;">

                <tr>
                  <td height="25"></td>
                </tr>
                <tr>
                  <td>

                    <table class="table1" width="520" align="center" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">

                      <tr>
                        <td height="25"></td>
                      </tr>
                      <tr>
                        <td>

                          <table width="50%" align="center" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td align="center">
                                <a href="#" class="editable-img">
                                  <img editable="true"  src="cid:image_1" width="357" style="display:block; line-height:0; font-size:0; border:0;"
                                      border="0" alt="logo" />
                                </a>
                              </td>
                            </tr>
                            <tr>
                              <td height="22"></td>
                            </tr>
                          </table>




                        </td>
                      </tr>



                    </table>

                  </td>
                </tr>


              </table>

            </td>
          </tr>

          <tr>
            <td>

              <table class="table1 editable-bg-color bg_color_ffffff" bgcolor="#ffffff" width="600" align="center" border="0" cellspacing="0"
                  cellpadding="0" style="margin: 0 auto;">

                <tr>
                  <td height="60"></td>
                </tr>

                <tr>
                  <td>

                    <table class="table1" width="520" align="center" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">




                      <tr>
                        <td height="10"></td>
                      </tr>

                      <tr>
                        <td  align="left" class="center_content text_color_a1a2a5" style="color: #a1a2a5; font-size: 14px;line-height: 2; font-weight: 500; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                          <div class="editable-text" style="line-height: 2;">
                            <span class="text_container">
                              <multiline>



                                Su concepto técnico emitido sobre el proyecto de ley "<xsl:value-of select="//@draft_law_title" />" ha sido aceptado podrá descargar su certificado correspondiente en su cuenta ARCA.
                              </multiline>
                            </span>
                          </div>
                        </td>
                      </tr>


                      <tr>
                        <td height="20"></td>
                      </tr>
                      <tr>
                        <td align="center">

                          <table class="button_bg_color_303f9f bg_color_303f9f" bgcolor="#303f9f" width="225" height="50" align="center" border="0"
                              cellpadding="0" cellspacing="0" style="background-color:#BC9E54; border-radius:3px;">
                            <tr>
                              <td  align="center" valign="middle" style="color: #ffffff; font-size: 16px; font-weight: 600; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;"
                                  class="text_color_ffffff">
                                <div class="editable-text">
                                  <span class="text_container">
                                    <multiline>
                                      <a href="{//@url_view_concept}" style="text-decoration: none; color: #ffffff;">Ingresar</a>
                                    </multiline>
                                  </span>
                                </div>
                              </td>
                            </tr>
                          </table>

                        </td>
                      </tr>



                      <tr>
                        <td height="45"></td>
                      </tr>
                      <tr>
                        <td  align="CENTER" class="center_content text_color_282828" style="color: #a1a2a5; font-size: 15px; font-weight: 700; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                          <div class="editable-text">
                            <span class="text_container">
                              <multiline>
                                ******No responder-Mensaje-generado automáticamente*******
                              </multiline>
                            </span>
                          </div>
                        </td>
                      </tr>



                      <tr>
                        <td height="20"></td>
                      </tr>


                    </table>

                  </td>
                </tr>


                <tr>
                  <td height="60"></td>
                </tr>
              </table>

            </td>
          </tr>
          <tr>
            <td>
              <table class="table1" width="520" align="center" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
                <tr>
                  <td  align="center" class="text_color_a1a2a5" style="color: #282828; font-size: 14px;line-height: 2; font-weight: 500; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                    <div class="editable-text" style="line-height: 2;">
                      <span class="text_container">
                        <multiline>
                          Declaración de Privacidad
                        </multiline>
                      </span>
                    </div>
                  </td>
                </tr>

                <tr>
                  <td height="5"></td>
                </tr>

                <tr>
                  <td  align="center" class="text_color_a1a2a5" style="color: #282828; font-size: 14px;line-height: 2; font-weight: 500; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                    <div class="editable-text" style="line-height: 2;">
                      <span class="text_container">
                        <multiline>
                          Ha recibido esta notificación única porque ha creado una cuenta en ARCA.
                        </multiline>
                      </span>
                    </div>
                  </td>
                </tr>


                <tr>
                  <td height="10"></td>
                </tr>

                <tr>
                  <td  align="center" class="text_color_a1a2a5" style="color: #282828; font-size: 12px;line-height: 2; font-weight: 500; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                    <div class="editable-text" style="line-height: 2;">
                      <span class="text_container">
                        <multiline>
                          <a href="{//@url_privacidad}" class="text_color_303f9f" style="color:#303f9f; text-decoration: none;"> Ver declaración de privacidad - </a>
                          <a href="{//@url_contacto}" class="text_color_303f9f" style="color:#303f9f; text-decoration: none;"> Contacto: xxx-xxx-xxx </a>
                          <a href="{//@url_politicas}" class="text_color_303f9f" style="color:#303f9f; text-decoration: none;"> Políticas de uso-términos y condiciones </a>
                        </multiline>
                      </span>
                    </div>
                  </td>
                </tr>



              </table>

            </td>
          </tr>
          <tr>
            <td>

              <table class="table1 editable-bg-color bg_color_303f9f" bgcolor="#BC9E54" width="600" align="center" border="0" cellspacing="0"
                        cellpadding="0" style="margin: 0 auto;">

                <tr>
                  <td height="15"></td>
                </tr>
                <tr>
                  <td>

                    <table class="table1" width="520" align="center" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
                      <tr>
                        <td>

                          <table width="100%" align="left" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td  align="justify" class="text_color_282828" style="color: #FFFFFF; font-size: 11px; font-weight: 700; font-family: lato, Helvetica, sans-serif; mso-line-height-rule: exactly;">
                                <div class="editable-text">
                                  <span class="text_container">
                                    <multiline>
                                      Este mensaje y todos los anexos transmitidos mediante el mismo han sido enviados por CAEL (Centro de Investigación y Altos
                                      Estudios Legislativos), para uso exclusivo del destinatario y
                                      se debe mantener confidencialidad con respecto a la información
                                      consignada. Si usted ha recibido este mensaje por error, le informamos
                                      que la difusión, distribución, copia u otro uso de este mensaje
                                      o sus anexos está estrictamente prohibido. Le agradecemos nos
                                      comunique inmediatamente y proceda a eliminarlo. (E-mail – Teléfonos)
                                    </multiline>
                                  </span>
                                </div>
                              </td>
                            </tr>
                            <tr>
                              <td height="12"></td>
                            </tr>
                          </table>




                        </td>
                      </tr>



                    </table>

                  </td>
                </tr>


              </table>

            </td>
          </tr>

        </table>


      </body>
    </html>

  </xsl:template>

</xsl:stylesheet>