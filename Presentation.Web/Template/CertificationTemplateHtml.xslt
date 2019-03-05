<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:formatHelper="urn:xsltExtension-FormatHelper">

  <xsl:output method="html" />

  <!-- Get config.file -->


  <xsl:template match="/">
    <HTML>
      <HEAD>
        <META http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
        <META http-equiv="X-UA-Compatible" content="IE=8"/>
        <TITLE>FORMATO CERTIFICACION</TITLE>
        <STYLE type="text/css">
          body {margin-top: 0px;margin-left: 0px;}
          #page_1 {position:relative; overflow: hidden;margin: 20px 0px 20px 20px;padding: 0px;border: none;width: 796px;height: 1016px;}
          #page_1 #p1dimg1 {position:absolute;top:0px;left:0px;z-index:-1;width:776px;height:1016px;}
          #page_1 #p1dimg1 #p1img1 {width:776px;height:1016px;}
          .dclr {clear:both;float:none;height:1px;margin:0px;padding:0px;overflow:hidden;}
          .ft0{font: bold 18px 'Constantia';line-height: 23px;}
          .ft1{font: bold 19px 'Constantia';line-height: 23px;}
          .ft2{font: 19px 'Constantia';line-height: 23px;}
          .ft3{font: 19px 'Constantia';text-decoration: underline;line-height: 23px;}
          .ft4{font: bold 19px 'Constantia';text-decoration: underline;line-height: 23px;}
          .ft5{font: 15px 'Calibri';line-height: 18px;}
          .p0{text-align: left;padding-left: 91px;padding-right: 82px;margin-top: 40px;margin-bottom: 0px;text-indent: -18px;}
          .p1{text-align: left;padding-right: 20px; padding-left: 40px;margin-top: 98px;margin-bottom: 0px;}
          .p2{text-align: left;padding-right: 20px;padding-left: 40px;margin-top: 1px;margin-bottom: 0px;}
          .p3{text-align: justify;padding-right: 20px;padding-left: 40px;padding-right: 40px;margin-top: 2px;margin-bottom: 0px;}
          .p4{text-align: left;padding-right: 20px;padding-left: 40px;margin-top: 45px;margin-bottom: 0px;}
          .p5{text-align: left;padding-right: 20px;padding-left: 40px;margin-top: 3px;margin-bottom: 0px;}
          .p6{text-align: left;padding-right: 20px;padding-left: 255px;margin-top: 145px;margin-bottom: 0px;}
          .p7{text-align: left;padding-right: 20px;padding-left: 225px;margin-top: 3px;margin-bottom: 0px;}
          .p8{text-align: left;padding-right: 20px;padding-left: 311px;margin-top: 4px;margin-bottom: 0px;}
          .p9{text-align: left;padding-right: 20px;padding-left: 10px;;margin-bottom: 0px;}
          .clearfix:after {
          content: "";
          display: table;
          clear: both;
          }
          header {
          padding: 10px 0;
          margin: 20px;
          border-bottom: 1px solid #AAAAAA;
          }

          #logo {
          float: left;
          margin-top: 8px;
          }

          #logo img {
          height: 120px;
          }

          #company {
          float: right;
          text-align: right;

          }
        </STYLE>
      </HEAD>
      <BODY>
        <div align="center"  style="margin:15px">
          <table style="border:1px solid #BC9E54; max-height:1016px;" cellpadding="0" cellspacing="0" width="776px" height="1016px">
            <thead>
              <tr>
                <td>
                  <header class="clearfix">
                    <div id="logo">
                      <img src="logoc.png"/>
                    </div>
                    <div id="company">
                      <img src="logo.png"/>
                    </div>

                  </header>
                  <P class="p0 ft0">SECRETARÍA GENERAL DEL SENADO DE LA REPÚBLICA DE COLOMBIA CENTRO DE INVESTIGACIONES Y ALTOS ESTUDIOS LEGISLATIVOS CONCEPTO SISTEMA DE APOYO A LA LABOR LEGISLATIVA - ARCA</P>
                </td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>
                  <P class="p1 ft2">
                    El (La) doctor(a) <SPAN class="ft1">
                      <xsl:value-of select="//@investigador"/>
                    </SPAN>, identificado (a) con cédula de
                  </P>
                  <P class="p2 ft2">
                    Ciudadanía <xsl:value-of select="//@cedula"/>, de la ciudad de <xsl:value-of select="//@ciudad"/>, y vinculado a <SPAN class="ft1">
                      (<xsl:value-of select="//@institucion"/>)
                    </SPAN>,
                    <SPAN class="ft2">del Grupo de investigación </SPAN>(<xsl:value-of select="//@grupo_vinculado"/>)<SPAN class="ft2">
                      ,
                      presento en calidad de investigador, concepto técnico sobre el proyecto de ley
                    </SPAN>
                    <SPAN class="ft3">(</SPAN><SPAN class="ft4">
                      <xsl:value-of select="//@draft_law_title"/> : <xsl:value-of select="//@draft_law_id"/>
                    </SPAN><SPAN class="ft3">)</SPAN>
                    <SPAN class="ft2"> como insumo general de apoyo a la labor legislativa, con fecha (</SPAN><xsl:value-of select="//@fecha_aceptacion_txt"/><SPAN class="ft2">).</SPAN>
                  </P>
                  <P class="p4 ft2">Se expide la certificación a los <xsl:value-of select="//@day_name"/> (<xsl:value-of select="//@day"/>) días del mes de <xsl:value-of select="//@month_name"/> de dos mil <xsl:value-of select="//@year_name"/> (<xsl:value-of select="//@year"/>).</P>
                  <P class="p6 ft1">GREGORIO ELJACH PACHECO</P>
                  <P class="p7 ft1">SECRETARIO GENERAL DEL SENADO</P>
                  <P class="p8 ft1">DIRECTOR– CAEL</P>

                </td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <td>
                  <table width="100%" cellpadding="0" cellspacing="0" style="padding-left:40px;padding-right:40px;padding-bottom:15px;">
                    <tr>
                      <td>
                        <img src="{//@qr_url}" width="120" />
                      </td>
                      <td width="100%" style="vertical-align: bottom;">


                      </td>
                      <td>
                        <img src="arcalogo.jpg"/>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            </tfoot>
          </table>
        </div>
      </BODY>
    </HTML>
  </xsl:template>

</xsl:stylesheet>
