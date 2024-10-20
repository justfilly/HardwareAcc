import os
import mysql.connector
import pandas as pd

host = 'localhost'
database = 'hardwareacc'
user = 'admin'
password = 'w7bNkR3vJiUg'


def get_parent_path(path, levels=1):
    parent_path = path
    for _ in range(levels):
        parent_path = os.path.dirname(parent_path)
    return parent_path


script_directory = os.path.dirname(os.path.abspath(__file__))

images_directory = os.path.join(get_parent_path(script_directory, 1))

excel_filename = 'Hardware_To_Insert.xlsx'
excel_file_path = os.path.join(get_parent_path(script_directory, 1),  excel_filename)
sheet_name = 'Sheet1'


def insert_data_from_excel():
    try:
        connection = mysql.connector.connect(
            host=host,
            database=database,
            user=user,
            password=password
        )

        if connection.is_connected():
            cursor = connection.cursor()

            df = pd.read_excel(excel_file_path, sheet_name=sheet_name)

            for index, row in df.iterrows():
                name = row['Name']
                image_filename = row['Image']
                inventory_number = row['InventoryNumber']
                price = row['Price']

                with open(os.path.join(images_directory, image_filename), 'rb') as file:
                    image_data = file.read()

                sql_insert_query = """INSERT INTO hardware 
                                      (name, inventory_number, image, price) 
                                      VALUES (%s, %s, %s, %s)
                                      ON DUPLICATE KEY UPDATE
                                      name = VALUES(name),
                                      image = VALUES(image),
                                      price = VALUES(price)"""

                cursor.execute(sql_insert_query, (name, inventory_number, image_data, price))
                connection.commit()

            print("Data inserted successfully")

    except mysql.connector.Error as e:
        print(f"Error while inserting data to MySQL: {e}")

    finally:
        if connection.is_connected():
            cursor.close()
            connection.close()
            print("MySQL connection is closed")


insert_data_from_excel()
