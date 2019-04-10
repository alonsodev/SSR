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
          @font-face {
          font-family: SourceSansPro;
          src: url(@url_site/Assets/SourceSansPro-Regular.ttf);
          }

          .clearfix:after {
          content: "";
          display: table;
          clear: both;
          }
          .div-with-bg
          {
          background: #DDDDDD;
          height: 70px;
          }
          a {
          color: #0087C3;
          text-decoration: none;
          }

          body {
          position: relative;
          width: 21cm;
          height: 29.7cm;
          margin: 0 auto;
          color: #555555;
          background: #FFFFFF;
          font-family: Arial, sans-serif;
          font-size: 14px;
          font-family: SourceSansPro;
          }

          header {
          padding: 10px 0;
          margin-bottom: 20px;
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


          .details {
          margin-bottom: 50px;
          }

          #client {
          float: right;
          padding-left: 6px;
          border-left: 6px solid #725030;
          margin-top: 15px;
          margin-right: 20px;

          }
          #client2 {
          padding-left: 6px;
          border-left: 6px solid #725030;
          float: left;
          }

          #client .to {
          color: #777777;
          }

          h2.name {
          font-size: 1.4em;
          font-weight: normal;
          margin: 0;
          }

          #invoice {
          float: left;
          text-align: right;

          color: #000;
          font-size: 12px;
          padding: 23px 5px 5px 16px;

          }
          #invoice2{
          float: left;
          text-align: right;

          font-size: 10px;
          color: #000;
          padding: 9px 5px 5px 22px;
          }

          #invoice h1 {
          color: #000;
          font-size: 20px;
          line-height: 1em;
          font-weight: normal;
          margin: 0  0 10px 0;
          }

          #invoice .date {
          font-size: 1.1em;
          color: #777777;
          }

          table {
          width: 100%;
          border-collapse: collapse;
          border-spacing: 0;
          margin-bottom: 20px;
          float: left;
          }

          table th,
          table td {
          padding: 5px;
          background: #EEEEEE;
          text-align: center;

          }

          table th {
          white-space: nowrap;
          font-weight: normal;
          }

          table td {
          text-align: left;
          border-right: 4px  solid #FFFFFF;
          }

          table td h3{
          color: #57B223;
          font-size: 1.2em;
          font-weight: normal;
          margin: 0 0 0.2em 0;
          }

          table .no {
          color: #FFFFFF;
          font-size: 1.6em;
          background: #BC9E54;
          border: 4px solid #FFFFFF;
          }

          table .desc {
          color: #FFFFFF;
          font-size: 1.6em;
          background: #BC9E54;
          border: 4px solid #FFFFFF;
          }

          table .unit {
          background: #DDDDDD;
          }

          table .qty {
          }

          table .total {
          background: #BC9E54;
          color: #FFFFFF;
          }

          table td.unit,
          table td.qty,
          table td.total {
          font-size: 1.2em;
          }

          table tbody tr:last-child td {
          border: none;
          }

          table tfoot td {
          padding: 10px 20px;
          background: #FFFFFF;
          border-bottom: none;
          font-size: 1.2em;
          white-space: nowrap;
          border-top: 1px solid #AAAAAA;
          }

          table tfoot tr:first-child td {
          border-top: none;
          }

          table tfoot tr:last-child td {
          color: #57B223;
          font-size: 1.4em;
          border-top: 1px solid #57B223;

          }

          table tfoot tr td:first-child {
          border: none;
          }

          #thanks{
          font-size: 20px;
          margin-bottom: 50px;
          text-align: center;
          }

          #notices{
          padding-left: 6px;
          border-left: 6px solid #ffc400;
          text-align: justify;
          }

          #notices .notice {
          font-size: 1.2em;
          }

          footer {
          color: #777777;
          width: 100%;
          height: 30px;

          bottom: 0;
          border-top: 1px solid #AAAAAA;
          padding: 8px 0;
          text-align: center;
          }

          .investigator_project td{
          max-width:50%;
          }

          #notices img{
          max-width: 100%;
          }
        </style>
      </head>
      <body>
        <main>
          <div i="" class="div-with-bg details clearfix">

            <div id="invoice">
              <h1>
                <strong>Concepto Técnico</strong>
              </h1>


            </div>
            <div id="client">
              <div class="to">
                # Concepto:  <xsl:value-of select="//@concept_id"/>
              </div>
              <div class="date">
                Fecha: <xsl:value-of select="//@fecha_presentacion_txt"/>
              </div>

            </div>
          </div>
          <br/>

          <div id="thanks">
            <xsl:value-of select="//@draft_law_title"/>
          </div>

          <div  class="clearfix">
            <table class="investigator_project" border="0" cellspacing="0" cellpadding="0">
              <thead>
                <tr>
                  <th class="no" style="width: 50%;">Datos del Investigador</th>
                  <th class="desc">Datos del Proyecto</th>

                </tr>
                <tr>
                  <td>
                    <xsl:value-of select="//@investigador"/>
                  </td>

                  <td>
                    <xsl:value-of select="//@draft_law_number"/>
                  </td>



                </tr>
                <tr>
                  <td>
                    <xsl:value-of select="//@cedula"/>
                  </td>

                  <td>
                    <xsl:value-of select="//@draf_law_origen"/>
                  </td>



                </tr>
                <tr>
                  <td>
                    <xsl:value-of select="//@institucion"/>
                  </td>

                  <td>
                    <xsl:value-of select="//@draf_law_fecha_presentacion_txt"/>
                  </td>


                </tr>
                <tr>
                  <td>
                    <xsl:value-of select="//@grupo_vinculado"/>
                  </td>

                  <td>
                    <xsl:value-of select="//@draf_law_commission"/>
                  </td>



                </tr>
                <tr>
                  <td>
                    <xsl:value-of select="//@codigo_grupo_vinculado"/>
                  </td>
                  <!-- CODIGO DEL GRUPO  -->
                  <td>
                    <xsl:value-of select="//@draf_law_interested_area"/>
                  </td>
                  <!-- TEMA  -->


                </tr>
              </thead>


            </table>
          </div>
          <div  class="div-with-bg details clearfix">

            <div id="invoice2">
              <h1>
                <strong>
                  Palabras claves
                </strong>
              </h1>

            </div>

          </div>



          <div id="notices">
            <xsl:value-of select="//@tag_names"/>
          </div>
          <br/>
          <br/>
          <br/>
          <br/>
          <div  class="div-with-bg details clearfix">

            <div id="invoice2">
              <h1>
                <strong>
                  Resumen
                </strong>
              </h1>

            </div>

          </div>



          <div id="notices">
            <xsl:value-of select="//@summary"/>
          </div>


          <br/>
          <br/>
          <br/>
          <br/>
          <div  class="div-with-bg details clearfix">

            <div id="invoice2">
              <h1>
                <strong>
                  Contenido Concepto Técnico
                </strong>
              </h1>

            </div>

          </div>



          <div id="notices">


            @concept
            <b>Bibliografía:</b>
            <xsl:value-of select="//@bibliography"/>
          </div>


        </main>
        <br/>
        <br/>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
